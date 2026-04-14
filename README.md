# 🤖 ChatbotApp – AI Chat & Image Generator (C# .NET)

A simple yet powerful console-based AI chatbot built with C# (.NET) using OpenAI API. It supports both text conversations and AI image generation, with local saving of generated images.

## 🚀 Features
- 💬 **AI-powered chat** using OpenAI GPT models.
- 🖼️ **AI image generation** from text prompts.
- 🧠 **Chat history memory** (context-aware responses).
- 📁 **Auto-save generated images** locally.
- ⚡ **Async / Await** for fast API calls.
- 🖥️ **Clean console UI** with commands support.

## 🛠️ Tech Stack
- **Language:** C# (.NET)
- **API:** OpenAI API
- **JSON Handling:** System.Text.Json
- **Networking:** HttpClient
- **Project Type:** Console Application

## 📌 How It Works

### 💬 Text Chat
1. User types a message.
2. Sent to OpenAI Chat Completions API.
3. Bot returns a smart, concise reply based on conversation history.

### 🖼️ Image Generation
1. Type command: `image <your prompt here>` (e.g., `image a futuristic robot in Cairo`).
2. Sends request to OpenAI Image API.
3. Saves the image locally inside the `/images` folder.

## ▶️ How to Run

1. **Clone the repository:**
   ```bash
   git clone [https://github.com/your-username/ChatbotApp.git](https://github.com/your-username/ChatbotApp.git)
   ------------------------------------

## Open the project in Visual Studio or VS Code.
## Add your OpenAI API key in Program.cs:
```c#
private static readonly string apiKey = "YOUR_API_KEY_HERE";
```
-------------------------------------
## 📁 Project Structure
Plaintext
ChatbotApp
│
├── Program.cs       # Main logic & API integration
├── images/          # Folder where generated images are stored
└── README.md        # Project documentation
-----------------------------------------
## ⚠️ Notes
Requires an active OpenAI API key.

An active internet connection is needed to communicate with the API.

Image generation depends on your API plan and model access (e.g., DALL-E 2 or DALL-E 3).
---------------------------
🔥 Example
Text Input:

What is JavaScript?

Bot Output:

JavaScript is a high-level, interpreted programming language primarily used for creating interactive elements within web browsers...

Image Input:

image cyberpunk city at night

Saved Output:

images/image_20260415_153000.png

👨‍💻 Author
Built by a .NET developer exploring AI integration with OpenAI APIs.

⭐ Future Improvements
GUI Version: Moving from Console to WPF or WinForms.

Streaming: Implementing a typing effect for responses.

Voice Support: Integration with Speech-to-Text and Text-to-Speech.

Advanced Memory: Implementing a long-term database for chat history.

📜 License
This project is open-source and free to use for learning and personal projects.
ذ
