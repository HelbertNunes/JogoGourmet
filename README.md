# Jogo Gourmet

Hello!

This is my version of the Jogo Gourmet example. In this README file, I will explain some of the development decisions I made during the process:

## Development Decisions

1. **Simplicity and Scope**:
    - I aimed to keep the code as simple as possible to adhere to the scope of the solution.

2. **Functionality**:
    - I couldn't manage to make the solution work properly. I am providing the text for code and structure evaluation.

3. **Solution Structure**:
    - My idea was to divide the solution into "yes" and "no" parts and try to make the selections in memory dynamically, but it didn't work out as planned.
    - My initial idea was to use a stack for everything. I'm not sure if it would have worked, but the other approach I tried did not.

4. **Data Persistence**:
    - Initially, I considered using a proper database or a text file to persist the added dishes. However, I don't think that is the point of the test, so I just made it in memory. This means every time the application is closed, it starts again from scratch.

5. **Dish and Description**:
    - New dishes cannot be created without a description. For instance, the given example "Bolo de chocolate" does not have a description. The application starts with "Feijão tropeiro," which has a description, and all subsequent inputs will require a description as well.

## Note

- The application is designed to run entirely in memory. No data is persisted between sessions.

---

Thank you for reviewing my solution. I appreciate any feedback on the code and structure.

---
