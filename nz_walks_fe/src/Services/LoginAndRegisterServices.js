export async function RegisterUser({ email, password, role }) {
  const response = await fetch("https://localhost:7163/api/Auth/Register", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({
      userName: email,
      password: password,
      roles: [role],
    }),
  });

  return response;
}

export async function Login({ UserName, password }) {
  const response = await fetch("https://localhost:7163/api/Auth/Login", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({
      UserName: UserName,
      password: password,
    }),
  });

  return response;
}
