import os
import sys
import traceback

# Fallback mechanism to keep the server alive even if the main app crashes
try:
    from Backend.main import app
except Exception as e:
    print("CRITICAL: Failed to import main application")
    traceback.print_exc()
    
    from fastapi import FastAPI
    from fastapi.responses import PlainTextResponse
    
    app = FastAPI()
    error_msg = traceback.format_exc()
    
    @app.get("/")
    @app.post("/{path:path}")
    async def catch_all(path: str):
        return PlainTextResponse(f"Server Deployment Error:\n\n{error_msg}", status_code=200)

if __name__ == "__main__":
    import uvicorn
    port = int(os.getenv("PORT", 8000))
    uvicorn.run(app, host="0.0.0.0", port=port)
