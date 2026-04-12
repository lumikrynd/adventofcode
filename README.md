This project uses dotnet user-secrets to store a session-key,
which in turn is used to automatically download the puzzle
inputs.

Set the session key by running the following command from the
root of the project.

```
dotnet user-secrets set --project Main/ "Session" "Very_Long_Session_Id_String"
```

The session cookie can be found by looking at the cookies send
when getting your puzzle input on adventofcode.com