# 0_Full_Plugin_Context.md

## 🎯 Goal

This document provides the **full context** for the Revit AI Plugin project.  
It is intended to be the **master reference** for any AI system tasked with coding, orchestrating, or maintaining the plugin.  
The AI must understand the purpose, architecture, folder structure, frontend/backend split, data flows, and all interconnected Markdown documents.

---

## 🧩 Project Overview

- **Purpose:** Create a Revit plugin that allows users to:
  1. Enter text commands for AI-driven modeling and automation.
  2. Upload images to provide visual context for AI workflows.
  3. Use Revit element selections as input for AI commands.
  4. Execute AI-generated workflows directly inside Revit.
  5. Provide feedback for AI learning and memory.

- **Outcome:** Users can issue commands like "create a section through selected stairs" or "dimension all walls in this plan" and the plugin will execute it safely in Revit, while logging, learning, and improving with user feedback.

---

## 🏗 Architecture Overview

The plugin is split into **two main layers**:  

### 1. Frontend (C#, WPF, Revit Add-in)
- **Purpose:** Capture user input, manage selection context, handle image uploads, display feedback.
- **Key Components:**
  - `CommandWindow.xaml` – main UI.
  - `CommandWindow.xaml.cs` – UI logic and event handling.
  - `FeedbackDialog.xaml` – user feedback dialog.
  - `DockablePanel` – optional docked UI in Revit.
- **Responsibilities:** Build JSON payloads containing command, selection, image context; send to backend; handle responses; execute feedback loop.

### 2. Backend (Python, FastAPI, Railway-hosted, Supabase for DB)
- **Purpose:** Interpret commands, generate actionable workflow JSON, maintain memory, store feedback.
- **Key Components:**
  - `AI Orchestrator` – central processing and routing of commands.
  - `LLM Client` – interface to OpenAI / Claude / other LLMs.
  - `Memory DB` – Supabase used to store:
    - command history
    - execution context
    - user feedback
  - `API Endpoints`:
    - `/api/interpret` – receive commands, selection, and images; return workflow JSON.
    - `/api/feedback` – store user feedback and update memory.
- **Responsibilities:** Generate valid JSON workflows, ensure deterministic outputs for repeated commands, handle multimodal input (text + images + selection), integrate with Supabase for persistence.

---

## 🌐 Communication & Data Flow

1. **Frontend:** User enters command, uploads image, selects elements.
2. **Request Builder:** Command + context (selection + image + active view) → JSON payload.
3. **HTTP Client:** Sends payload to backend `/api/interpret`.
4. **Backend:** AI interprets command, generates workflow JSON.
5. **Execution Engine:** Workflow JSON returned → Frontend executes via Revit API.
6. **Feedback:** Post-execution dialog collects user feedback → sent to backend `/api/feedback` → updates Supabase memory.

[User Input + Selection + Image] → Frontend → JSON Payload → Backend AI → Workflow JSON → Frontend Execution → Feedback → Memory

yaml


---

## 📂 Folder & File Orchestration

### Suggested top-level structure:

/RevitAIPlugin
/Frontend
├── CommandWindow.xaml
├── CommandWindow.xaml.cs
├── FeedbackDialog.xaml
├── FeedbackDialog.xaml.cs
├── ViewModels/
│ └── CommandViewModel.cs
└── Resources/
├── execute.png
├── upload.png
└── select.png

/Backend
├── main.py # FastAPI entrypoint
├── ai_orchestrator.py # Central workflow generator
├── llm_client.py # Handles LLM calls
├── memory.py # Supabase interface
├── models.py # Pydantic models for request/response
└── utils/
├── image_processing.py
├── validation.py
└── logging.py

/Docs
├── 0_Full_Plugin_Context.md
├── 1.1_User_Interface.md
├── 1.2_Command_Handler.md
├── 1.3_Execution_Engine.md
├── 2.1_Request_Builder.md
├── 2.2_HTTP_Client.md
├── 2.3_Response_Parser.md
├── 3.1_API_Layer.md
├── 3.2_AI_Orchestrator.md
├── 3.3_LLM_Client.md
├── 4.1_Base_Structure.md
├── 4.2_Action_Signature.md
├── 4.3_Action_Registry.md
├── 4.4_Example_Action.md
├── 5.1_Storage_Model.md
├── 5.2_Retrieval_Logic.md
├── 5.3_Feedback_API.md
├── 6.1_Local_Development.md
├── 6.2_Server_Deployment.md
├── 6.3_Plugin_Deployment.md
└── 6.4_Logging.md

markdown


> The AI must respect this folder structure and generate code and workflows accordingly. All generated files must be placed in the correct folder.

---

## 🧠 AI Instructions / Context

1. **Understand the Goal:** This plugin is intended to automate Revit workflows using AI. Users can input text, images, and selections to control Revit actions.  
2. **Respect Separation of Concerns:**  
   - Frontend = UI + request building + execution.  
   - Backend = AI interpretation + workflow generation + memory.  
3. **Memory System:** Use Supabase to track all executed commands, context snapshots, and user feedback.  
4. **Multimodal Input:** AI must handle text + image + selection context.  
5. **JSON Workflows:** All instructions sent back to the frontend must be valid, type-safe JSON that the C# execution layer can interpret.  
6. **Error Handling:** Always validate inputs and selections. Provide fallback messages if user input is incomplete or invalid.  
7. **Folder Awareness:** AI must generate new files in the correct frontend/backend folder, referencing the existing Markdown documentation.  
8. **Deployment Context:** Backend is hosted on Railway.com, frontend runs inside Revit, communication via HTTP POST.  
9. **Future Proofing:** AI should be aware that additional actions, commands, or modules may be added, and all outputs should remain modular and maintainable.

---

## 📝 Key Notes for AI

- Supabase is the memory and feedback database.  
- Railway hosts the Python FastAPI backend.  
- Frontend is C# WPF inside Revit.  
- Every AI-generated workflow must include:
  1. Validation of selection and context
  2. Optional image analysis
  3. Safe Revit API execution
  4. Logging and feedback collection

- All Markdown docs under `/Docs` are references; AI must cross-reference them to understand module responsibilities and integration points.

---

## ✅ Objective for AI

Given this full context, AI should be able to:

1. Generate new frontend C# WPF code and UI components.  
2. Generate backend Python code for FastAPI endpoints, LLM orchestration, and Supabase memory integration.  
3. Maintain correct folder structure and module separation.  
4. Produce type-safe JSON workflows compatible with the frontend execution engine.  
5. Respect multimodal inputs (text, selection, image) and feedback loops.  
6. Generate code that is maintainable, modular, and ready for deployment on Railway.  

This file is the **master context** for all plugin development tasks.
