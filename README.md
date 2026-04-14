🤖 ChatbotApp – AI Chat & Image Generator (C# .NET)

A simple yet powerful console-based AI chatbot built with C# (.NET) using OpenAI API.
It supports both text conversations and AI image generation, with local saving of generated images.

🚀 Features
💬 AI-powered chat using OpenAI GPT models
🖼️ AI image generation from text prompts
🧠 Chat history memory (context-aware responses)
📁 Auto-save generated images locally
⚡ Async / Await for fast API calls
🖥️ Clean console UI with commands support
🛠️ Tech Stack
C# (.NET)
OpenAI API
System.Text.Json
HttpClient
Console Application
📌 How It Works
💬 Text Chat
User types a message
Sent to OpenAI Responses API
Bot returns a smart concise reply
🖼️ Image Generation
Type command:
image a futuristic robot in Cairo
Sends request to OpenAI Image API
Saves image locally inside:
/images
▶️ How to Run
1. Clone repo
git clone https://github.com/your-username/ChatbotApp.git
2. Open project in Visual Studio
3. Add your OpenAI API key
private static readonly string apiKey = "YOUR_API_KEY";
4. Run project
dotnet run
💡 Commands
Command	Description
text input	Chat with AI
image <prompt>	Generate image
exit	Close the app
📁 Project Structure
ChatbotApp
│
├── Program.cs
├── images/         # Generated images stored here
└── README.md
⚠️ Notes
Requires active OpenAI API key
Internet connection needed
Image generation depends on model access (gpt-image-1-mini or similar)
🔥 Example
Input:
What is JavaScript?
Output:
JavaScript is a high-level programming language used for web development...
Image Example:
image cyberpunk city at night

Saved output:

images/image_20260415_153000.png
👨‍💻 Author

Built by a .NET developer exploring AI integration with OpenAI APIs.

⭐ Future Improvements
GUI version (WPF / WinForms)
Streaming responses (like ChatGPT typing effect)
Voice input/output
Better memory system (long-term chat history)
📜 License

Free to use for learning and personal projects.