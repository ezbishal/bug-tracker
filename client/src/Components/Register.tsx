import { DefaultButton, Label, TextField } from "@fluentui/react";
import axios from "axios";
import React from "react";

const Register: React.FC = () => {
  const baseUrl: string = "https://localhost:7109/api";

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const firstName = (document.querySelector("#firstName") as HTMLInputElement)
      .value;
    const lastName = (document.querySelector("#lastName") as HTMLInputElement)
      .value;
    const email = (document.querySelector("#email") as HTMLInputElement).value;
    const password = (document.querySelector("#password") as HTMLInputElement)
      .value;

    axios
      .post(`${baseUrl}/users/register`, {
        firstName,
        lastName,
        email,
        password,
      })
      .then((response) => {
        window.location.href = "/login";
      });
  };

  return (
    <form onSubmit={handleSubmit}>
      <div className="flex items-center justify-center h-screen">
        <div className="border-2 rounded-md p-3 flex flex-col w-fit gap-5 m-auto">
          <div>
            <Label htmlFor="firstName" className="mr-5">First Name:</Label>
            <TextField id="firstName" type="text" className="border mr-5" />
          </div>
          <div>
            <Label htmlFor="lastName" className="mr-5">Last Name:</Label>
            <TextField id="lastName" type="text" className="border p-1" />
          </div>
          <div>
            <Label htmlFor="email" className="mr-5">Email:</Label>
            <TextField id="email" type="email" className="border p-1" />
          </div>
          <div>
            <Label htmlFor="password" className="mr-5">Password:</Label>
            <TextField id="password" type="password" className="border p-1" />
          </div>
          <DefaultButton type="submit">
            Register
          </DefaultButton>
        </div>
        </div>
    </form>
  );
};

export default Register;
