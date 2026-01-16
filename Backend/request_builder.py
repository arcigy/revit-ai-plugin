import json
from datetime import datetime
import uuid
from typing import List, Optional
from models import SelectedElement, ImageContext

class RequestBuilder:
    @staticmethod
    def build_payload(command_text: str, active_view: str,
                      selected_elements: List[SelectedElement],
                      image_context: Optional[ImageContext] = None) -> str:
        request_id = str(uuid.uuid4())
        payload = {
            "request_id": request_id,
            "timestamp": datetime.utcnow().isoformat(),
            "command_text": command_text,
            "context": {
                "active_view": active_view,
                "selected_elements": [e.dict() for e in selected_elements]
            },
            "image_context": image_context.dict() if image_context else None
        }
        return json.dumps(payload)
