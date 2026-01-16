from models import CommandResponse, Workflow, ActionStep

def parse_response(response_json: dict) -> CommandResponse:
    try:
        # Check if "workflow" key exists, otherwise assume structure might be different or flat
        if "workflow" in response_json:
            workflow_data = response_json["workflow"]
        else:
             # Fallback if structure is different
             workflow_data = response_json
        
        # Ensure 'steps' exist
        if "steps" not in workflow_data:
             # If no steps, check for errors directly
             if "errors" in response_json:
                 return CommandResponse(errors=response_json["errors"])
             raise ValueError("No steps found in workflow response")

        steps = [ActionStep(command=step["command"], parameters=step["parameters"])
                 for step in workflow_data["steps"]]
        workflow = Workflow(steps=steps)
        errors = response_json.get("errors", [])
        return CommandResponse(workflow=workflow, errors=errors)
    except KeyError as e:
        raise ValueError(f"Missing expected key in response: {e}")
    except Exception as e:
        return CommandResponse(errors=[str(e)])
