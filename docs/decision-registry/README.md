# Decision Register

Briefly, a decision register is a structured document or tool used to record and track key decisions made during a project, program, or initiative. It serves as a centralized log to capture the context, rationale, options considered, and outcomes of decisions, ensuring transparency, accountability, and traceability. It is commonly used in project management, software development, and solutions architecture to document critical choices, such as technical, strategic, or operational decisions.

> [!NOTE]
> Take note of the following details relating to _Decisions_:  
> 
> - All decisions relating to this project are captured in the [./decisions](./decisions/) folder.
> - See the accompanying [Decision Register Template](decision-template.md) for an example of how to document a decision.
> - See the [Standard Operating Procedure (SOP) - Add Decision](./decision-sop.md) for the procedure on recording a decision.
> - Decision ID's are based on the [Decision ID Convention](#decision-id-convention) below.
> - When adding a new decision, please ensure to add the decision to the [Decision Table](./decision-summary.md#decision-table) located in the [Decision Summary document](./decision-summary.md).

---

## Decision ID Convention

A _Decision ID_ looks like: `DEC-1010`.

The ID format consists of two parts separated by a hyphen (–):

- Prefix: A fixed string "DEC", which stands for "DECision"
- Number: A four-digit number ####, ranging from 0000 to 9999, where leading zeros are used to ensure the number is always four digits long (e.g., 0001 for 1, 0123 for 123).

For example:

- `DEC-0001` represents first Decision ID
- `DEC-0123` represents 123rd Decision ID.

The format ensures consistency and allows for up to 9,999 unique IDs (0001 to 9999).

> [!IMPORTANT]
> A _Decision_ file name follows the same convention as the convention used (above) for Decision ID, with the exception that all alphabetic characters are lowercase.  
> 
> For example: The Decision ID of `DEC-1234` translates to `dec-1234.md` for filename

---