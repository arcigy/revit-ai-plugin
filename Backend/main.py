import os
from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
from typing import List, Optional, Dict

# from dotenv import load_dotenv
# load_dotenv()

from ai_orchestrator import AIOrchestrator
from models import CommandRequest, CommandResponse, FeedbackRequest

SUPABASE_URL = os.getenv("SUPABASE_URL", "https://placeholder.supabase.co")
SUPABASE_KEY = os.getenv("SUPABASE_KEY", "FAKE_KEY_123")
AI_API_URL = os.getenv("AI_API_URL", "https://api.placeholder.ai/v1")
AI_API_KEY = os.getenv("AI_API_KEY", "FAKE_AI_KEY_456")

app = FastAPI()
orchestrator = AIOrchestrator()

@app.post("/api/interpret", response_model=CommandResponse)
async def interpret_command(request: CommandRequest):
    """
    Interprets the user command using the AI orchestrator.
    """
    print(f"Received command: {request.command_text}")
    response = await orchestrator.process_command(request)
    if response.errors:
        raise HTTPException(status_code=500, detail=response.errors)
    return response

@app.post("/api/feedback")
async def receive_feedback(feedback: FeedbackRequest):
    """
    Receives feedback and stores it using the memory manager.
    """
    print(f"Received feedback for command: {feedback.command_text}")
    # In a real app, you'd likely need a request_id to associate feedback with a command
    # For now, we'll just log it.
    orchestrator.memory_manager.store_feedback("placeholder_request_id", feedback.success, feedback.note)
    return {"status": "feedback received"}

@app.get("/")
async def root():
    return {"message": "Revit AI Plugin Backend is running!"}
