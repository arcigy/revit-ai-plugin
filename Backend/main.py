print("--- LOADING MAIN.PY ---")
import os
import sys

# Print current directory and path for debugging
print(f"Current Directory: {os.getcwd()}")
print(f"Python Path: {sys.path}")

try:
    from fastapi import FastAPI, HTTPException
    from pydantic import BaseModel
    from typing import List, Optional, Dict
    print("--- IMPORTS: FASTAPI OK ---")
except Exception as e:
    print(f"--- CRITICAL IMPORT ERROR (FastAPI): {e}")

# Standard imports for running as a module from root
from Backend.ai_orchestrator import AIOrchestrator
from Backend.models import CommandRequest, CommandResponse, FeedbackRequest

# ... Rest of the file ...

SUPABASE_URL = os.getenv("SUPABASE_URL", "https://placeholder.supabase.co")
SUPABASE_KEY = os.getenv("SUPABASE_KEY", "FAKE_KEY_123")
AI_API_URL = os.getenv("AI_API_URL", "https://api.placeholder.ai/v1")
AI_API_KEY = os.getenv("AI_API_KEY", "FAKE_AI_KEY_456")

app = FastAPI()

# Global orchestrator variable
orchestrator = None

@app.on_event("startup")
async def startup_event():
    global orchestrator
    print("--- SERVER STARTUP EVENT FIRED ---")
    try:
        print("Initializing AI Orchestrator...")
        orchestrator = AIOrchestrator()
        print("AI Orchestrator initialized successfully.")
    except Exception as e:
        print(f"CRITICAL ERROR initializing Orchestrator: {e}")
        import traceback
        traceback.print_exc()
        orchestrator = None

@app.get("/")
async def root():
    return {"message": "Revit AI Plugin Backend is running!", "status": "ok"}

@app.post("/api/interpret", response_model=CommandResponse)
async def interpret_command(request: CommandRequest):
    """
    Interprets the user command using the AI orchestrator.
    """
    if orchestrator is None:
        raise HTTPException(status_code=500, detail="Backend is not fully initialized. Check server logs.")
        
    print(f"Received command: {request.command_text}")
    try:
        response = await orchestrator.process_command(request)
        if response.errors:
            raise HTTPException(status_code=500, detail=response.errors)
        return response
    except Exception as e:
        print(f"Error processing command: {e}")
        raise HTTPException(status_code=500, detail=str(e))

@app.post("/api/feedback")
async def receive_feedback(feedback: FeedbackRequest):
    """
    Receives feedback and stores it using the memory manager.
    """
    if orchestrator is None:
        return {"status": "error", "message": "Orchestrator not initialized"}

    print(f"Received feedback for command: {feedback.command_text}")
    try:
        orchestrator.memory_manager.store_feedback("placeholder_request_id", feedback.success, feedback.note)
        return {"status": "feedback received"}
    except Exception as e:
        print(f"Error storing feedback: {e}")
        return {"status": "error", "detail": str(e)}
