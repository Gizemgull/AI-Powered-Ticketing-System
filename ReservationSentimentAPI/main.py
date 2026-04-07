from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
from transformers import pipeline, AutoModelForSequenceClassification, AutoTokenizer
from typing import Optional
import uvicorn

app = FastAPI()
sentiment_pipeline: Optional[pipeline] = None

class TextPayload(BaseModel):
    text: str

@app.on_event("startup")
async def load_model():
    global sentiment_pipeline
    MODEL_NAME = "savasy/bert-base-turkish-sentiment-cased"
    print(f"[{MODEL_NAME}] YUKLENIYOR...")
    try:
        tokenizer = AutoTokenizer.from_pretrained(MODEL_NAME)
        model = AutoModelForSequenceClassification.from_pretrained(MODEL_NAME)
        sentiment_pipeline = pipeline("sentiment-analysis", model=model, tokenizer=tokenizer)
        print("✅ MODEL HAZIR! Artik yorum gonderebilirsiniz.")
    except Exception as e:
        print(f"❌ Hata: {e}")

# --- TÜRKÇE KARAKTER UYUMLU KÜÇÜLTME ---
def turkish_lower(text: str) -> str:
   
    text = text.replace("İ", "i").replace("I", "ı")
    return text.lower()

# --- NÖTR KELİME LİSTESİ ---
def check_neutral_phrases(text: str) -> bool:
    clean_text = turkish_lower(text)
    
    # Bu kelimeler geçerse DİREKT Nötr basar.
    neutral_triggers = [
        "idare eder", "ne iyi ne kötü", "fena değil", "fena sayılmaz",
        "ortalama", "eh işte", "standart", "vasat", "normaldi", 
        "çok beklentiniz olmasın", "sıradan", "orta şeker", "kötü sayılmaz"
    ]
    
    print(f"DEBUG: İncelenen Metin (Küçük): {clean_text}")
    
    for phrase in neutral_triggers:
        if phrase in clean_text:
            print(f"✅ LOG: Nötr kalıp yakalandı! -> '{phrase}'")
            return True
            
    print("❌ LOG: Nötr kalıp bulunamadı.")
    return False

# --- KİNAYE KONTROLÜ ---
def check_irony(text: str, current_label: str) -> str:
    clean_text = turkish_lower(text)
    
    if "(!)" in text:
        return "Negative"
    
    negative_triggers = [
        "hatırlayamayınca", "unuttular", "fiyasko", "rezalet", 
        "berbat", "sıkıcı", "uyudum", "pişmanım", "yazık", "çöp",
        "hayal kırıklığı", "değmez", "beğenmedim", "soğuktu"
    ]
    
    if current_label.upper() == "POSITIVE":
        for word in negative_triggers:
            if word in clean_text:
                print(f"✅ LOG: Kinaye yakalandı! -> '{word}'")
                return "Negative"

    return current_label 

@app.post("/api/sentiment/analyze")
async def analyze_sentiment(payload: TextPayload):
    if sentiment_pipeline is None:
        raise HTTPException(status_code=503, detail="Model yukleniyor.")
    
    print("-" * 30) 
    
    if check_neutral_phrases(payload.text):
        print("SONUÇ: Neutral (Manuel Liste)")
        return {"sentiment": "Neutral"}

    # BERT Analizi
    result = sentiment_pipeline(payload.text)[0]
    raw_label = result['label']
    score = result['score']
    
    print(f"DEBUG: Model Çıktısı: {raw_label} | Güven Puanı: {score:.4f}")

    #  Kinaye Kontrolü
    checked_label = check_irony(payload.text, raw_label)
    if checked_label == "Negative" and raw_label.upper() == "POSITIVE":
        print("SONUÇ: Negative (Kinaye)")
        return {"sentiment": "Negative"}

    #  Puan Düşüklüğü Kontrolü 
    
    if score < 0.70:
        print("SONUÇ: Neutral (Düşük Puan)")
        return {"sentiment": "Neutral"}

    #  Nihai Sonuç
    final_label = checked_label.capitalize()
    
    if "pos" in final_label.lower():
        final_label = "Positive"
    elif "neg" in final_label.lower():
        final_label = "Negative"

    print(f"SONUÇ: {final_label} (Model Kararı)")
    return {"sentiment": final_label}

if __name__ == "__main__":
    uvicorn.run(app, host="0.0.0.0", port=8000)