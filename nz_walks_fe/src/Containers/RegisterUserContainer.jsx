import { Button, TextField, Container, Grid } from "@mui/material";
import { useState } from "react";
import { RegisterUser } from "../Services/LoginAndRegisterServices";
import { useNavigate } from "react-router-dom";

export function RegisterUserContainer() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [role, setRole] = useState("");
  const navigate = useNavigate();

  const onSubmit = async () => {
    const res = await RegisterUser({
      email: email,
      password: password,
      role: role,
    });
    if (res.status === 200) {
      navigate("/");
    } else {
      console.log("unable to register user");
    }
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
      <Grid>
        <TextField
          onBlur={(e) => setRole(e.target.value)}
          label={"role"}
        ></TextField>
      </Grid>
      <Button onClick={() => onSubmit()}>Register</Button>
    </Container>
  );
}
