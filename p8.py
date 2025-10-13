import google.generativeai as genai

genai.configure(api_key="")
model = genai.GenerativeModel("models/gemini-2.5-flash-lite")
chat = model.start_chat(history=[])
response = chat.send_message("Hello, how are you?")
print(response.text)
