import httpx
import json
from models import CommandResponse
from response_parser import parse_response

class HTTPClient:
    def __init__(self, base_url: str, api_key: str, timeout: int = 30):
        self.base_url = base_url
        self.api_key = api_key
        self.timeout = timeout

    async def send_request(self, endpoint: str, payload: dict) -> dict:
        headers = {
            "Authorization": f"Bearer {self.api_key}",
            "Content-Type": "application/json"
        }
        async with httpx.AsyncClient(timeout=self.timeout) as client:
            try:
                response = await client.post(f"{self.base_url}/{endpoint}", json=payload, headers=headers)
                response.raise_for_status()
                return response.json()
            except httpx.RequestError as e:
                raise ConnectionError(f"Request failed: {e}") from e
            except httpx.HTTPStatusError as e:
                raise RuntimeError(f"Invalid response [{e.response.status_code}]: {e.response.text}") from e
