import { Button, TextField, Container, Grid } from "@mui/material";
import { useState } from "react";
import { Login } from "../Services/LoginAndRegisterServices";
import { useNavigate } from "react-router-dom";

export function LoginContainer() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const onSubmit = async () => {
    const response = await Login({ UserName: email, password: password });
    const data = await response.json();
    localStorage.setItem("token", data.jwtToken);
    navigate("/Region");
  };

  return (
    <Container>
      <Grid>
        <TextField
          onBlur={(e) => setEmail(e.target.value)}
          label={"email"}
        ></TextField>
      </Grid>

      <Grid>
        <TextField
          onBlur={(e) => setPassword(e.target.value)}
          label={"password"}
        ></TextField>
      </Grid>

      <Button onClick={() => onSubmit()}>Login</Button>
      <Button onClick={() => navigate("/Register")}>Register</Button>
    </Container>
  );
}
