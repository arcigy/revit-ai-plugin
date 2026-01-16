# Use an official lightweight Python image
FROM python:3.11-slim

# Set the working directory in the container
WORKDIR /app

# Prevent Python from writing pyc files and buffering stdout
ENV PYTHONDONTWRITEBYTECODE=1
ENV PYTHONUNBUFFERED=1
# Add /app to PYTHONPATH so imports like 'from Backend...' work correctly
ENV PYTHONPATH=/app

# Install system dependencies if needed (e.g. for building packages)
# RUN apt-get update && apt-get install -y --no-install-recommends gcc && rm -rf /var/lib/apt/lists/*

# Copy requirements and install dependencies
COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

# Copy the rest of the application code
COPY . .

# Expose the port that Railway will provide (defaulting to 8080)
ENV PORT=8080
EXPOSE $PORT

# Start the application using uvicorn directly
# We use shell form to allow variable expansion for $PORT, but exec form is generally better. 
# Here we use python -m uvicorn to ensure it's found in path.
CMD sh -c "python -m uvicorn app:app --host 0.0.0.0 --port ${PORT}"
