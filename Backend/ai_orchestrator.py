from Backend.memory import MemoryManager
from Backend.llm_client import LLMClient
from Backend.models import CommandRequest, CommandResponse, Workflow, ActionStep

class AIOrchestrator:
    def __init__(self):
        self.memory_manager = MemoryManager()
        self.llm_client = LLMClient()

    async def process_command(self, request: CommandRequest) -> CommandResponse:
        """
        Orchestrates the process of generating a workflow from a command.
        """
        print("--- Orchestrating command processing ---")
        
        # 1. (Optional) Retrieve relevant history from memory
        # relevant_history = self.memory_manager.retrieve_history(request.command_text)

        # 2. Generate workflow from LLM
        llm_response = await self.llm_client.generate_workflow(request)

        if not llm_response or "workflow" not in llm_response:
            return CommandResponse(errors=["Failed to generate workflow from AI."])

        # 3. Parse and validate the workflow
        try:
            workflow_data = llm_response['workflow']
            steps = [ActionStep(**step) for step in workflow_data['steps']]
            workflow = Workflow(steps=steps)
            response = CommandResponse(workflow=workflow)
        except Exception as e:
            return CommandResponse(errors=[f"Failed to parse AI workflow: {e}"])

        # 4. Store the command and response in memory
        self.memory_manager.store_history(request, response)
        
        print("-----------------------------------------")
        return response
