from typing import List
from models import SelectedElement

def validate_command(command_text: str, selected_elements: List[SelectedElement]):
    if not command_text.strip():
        raise ValueError("Command text cannot be empty.")
    # Example validation: enforce selection for specific commands if we could parse intent
    # if "select" in command_text.lower() and not selected_elements:
    #     raise ValueError("At least one element must be selected.")
