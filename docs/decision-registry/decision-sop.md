# Standard Operating Procedure (SOP) - Add Decision

## Title

Add Decision to _Decision Register_

---

## Purpose

This SOP outlines the steps for recording a new decision in the [Decision Register] to keep track of key decisions made throughout the development of the [BNL project](https://github.com/drminnaar/bnl).

---

## Prerequisites

In order to capture the required information for the [Decision Template](./decision-template.md), the following information will be required:

- Decision ID: A unique identity based on naming convention
- Decision Title: A brief name for decision
- Owner: The person that initiates the request for a decision
- Approver: The person that has reviewed and approves of decision proposal
- Context
- Thorough analysis of decision in order to provide:
  - considered options
  - evaluation criteria
  - decision proposal/outcome

---

## Process

- Make copy of [Decision Template](./decision-template.md) to [`./decisions`](./decisions/) folder.
- Name the new decision file with the `Decision ID` eg., `dec-1023.md`
- The name of file must be lowercase and follow the format of `dec-####` where '#' represents a number.
- The filename must be unique. Therefore, if there is a name clash, you need to re-evaluate the chosen Decision ID.
- Complete all the required sections in the new decision.
- Add entry of new decision to [README - Decision Summary](./README.md#decision-summary)

---

[Decision Register]: ./decisions