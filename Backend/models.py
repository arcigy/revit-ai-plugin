from pydantic import BaseModel
from typing import List, Optional, Dict

class SelectedElement(BaseModel):
    id: int
    name: str
    category: str

class ImageContext(BaseModel):
    filename: str
    base64: str

class Context(BaseModel):
    active_view: str
    selected_elements: List[SelectedElement]

class CommandRequest(BaseModel):
    command_text: str
    context: Context
    image_context: Optional[ImageContext] = None

class ActionStep(BaseModel):
    command: str
    parameters: Dict

class Workflow(BaseModel):
    steps: List[ActionStep]

class CommandResponse(BaseModel):
    workflow: Optional[Workflow] = None
    errors: Optional[List[str]] = None

class FeedbackRequest(BaseModel):
    command_text: str
    success: bool
    note: Optional[str] = None
