import os
import sys

# DEBUG: Print environment info immediately
print(f"--- STARTUP DEBUG ---")
print(f"Python: {sys.version}")
print(f"CWD: {os.getcwd()}")
print(f"Files in CWD: {os.listdir('.')}")
print(f"Environment Keys: {list(os.environ.keys())}")

try:
    from fastapi import FastAPI
    import uvicorn
    print("--- IMPORTS SUCCESSFUL ---")
except ImportError as e:
    print(f"--- FAILED TO IMPORT DEPENDENCIES: {e} ---")
    sys.exit(1)

app = FastAPI()

@app.get("/")
def root():
    return {
        "status": "online",
        "message": "Debug mode active. The server is running.",
        "env": {
            "cwd": os.getcwd(),
            "python": sys.version
        }
    }

@app.post("/api/interpret")
def dummy_interpret(data: dict):
    print(f"Received data: {data}")
    return {
        "workflow": {
            "steps": [
                {"command": "ShowMessage", "parameters": {"message": "DEBUG MODE: Server is reachable!"}}
            ]
        }
    }

if __name__ == "__main__":
    port = int(os.getenv("PORT", 8000))
    print(f"--- STARTING SERVER ON PORT {port} ---")
    uvicorn.run(app, host="0.0.0.0", port=port)
