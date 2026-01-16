import os
import sys
import traceback

# Fallback mechanism to keep the server alive even if the main app crashes
try:
    # Explicitly add current directory to path just in case
    current_dir = os.path.dirname(os.path.abspath(__file__))
    if current_dir not in sys.path:
        sys.path.append(current_dir)
        
    print(f"--- ATTEMPTING TO IMPORT BACKEND ---")
    print(f"CWD: {os.getcwd()}")
    print(f"Path: {sys.path}")
    
    from Backend.main import app
    print("--- SUCCESS: Backend.main imported ---")
    
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
        return PlainTextResponse(f"Deployment Error - Backend Import Failed:\n\n{error_msg}", status_code=500)

if __name__ == "__main__":
    import uvicorn
    port = int(os.getenv("PORT", 8000))
    print(f"--- STARTING SERVER ON PORT {port} ---")
    uvicorn.run(app, host="0.0.0.0", port=port)
