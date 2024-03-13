import { FormEvent, memo, useEffect, useState } from "react";
import axios, { AxiosResponse } from "axios";
import React from "react";
import UserSession from "../Models/UserSession";
import { ComboBox, DefaultButton, IComboBox, IComboBoxOption, Label, TextField } from "@fluentui/react";


const Users = () => {
  const baseUrl: string = "https://localhost:7109/api";
  const [users, setUsers] = useState<UserSession[]>([]);
  const [selectedUser, setSelectedUser] = useState<UserSession | null>(null);
  const [firstName, setFirstName] = useState<string>('');
  const [lastName, setLastName] = useState<string>('');

  useEffect(() => {
    axios
      .get(`${baseUrl}/users`)
      .then((response: AxiosResponse<UserSession[]>) => {
        console.log(response.data);
        setUsers(response.data);
        setSelectedUser(response.data[0]);
      });
  }, []);

  const handleSelectionChange = (
    event: FormEvent<IComboBox>,
    option: IComboBoxOption | undefined
  ) => {
    const user = users.find(
      (user: UserSession) => user.email === option?.key as string
    );
    if (user) {
      setSelectedUser(user);
      setFirstName(user.firstName);
      setLastName(user.lastName);
    }
  };

  function handleDelete(
    event: React.MouseEvent<HTMLButtonElement, MouseEvent>
  ): void {
    if (!selectedUser) return;
    const email = selectedUser.email;
    axios
      .delete(`${baseUrl}/users/${email}`)
      .then((response: AxiosResponse<string>) => {
        alert(response.data);
        window.location.href = "/";
      });
  }

  function handleSave(event: React.MouseEvent<HTMLButtonElement, MouseEvent>): void
  {
    if (!selectedUser) return;
    const email = selectedUser.email;
    const user = {firstName, lastName};
    axios
      .put(`${baseUrl}/users/${email}`, user)
      .then((response: AxiosResponse<string>) => {
        alert(response.data);
        window.location.href = "/";
      });
  }

  return (
    <div className="flex flex-col gap-5 m-5">
      <h3 className="text-[20px]">Users</h3>
        <Label htmlFor="users">Select User:</Label>
        <ComboBox id="users" className="border h-[40px]" 
          onChange={handleSelectionChange} options={[]}>
          {users.map((user: UserSession) => (
            <option value={user.email}>
              {user.firstName} {user.lastName}
            </option>
          ))}
        </ComboBox>

        <Label htmlFor="firstName">First Name:</Label>
        <TextField id="firstName" className="border" type="text" value={firstName} onChange={(e) => setFirstName((e.target as HTMLInputElement).value)} />
        <Label htmlFor="lastName">Last Name:</Label>
        <TextField id="lastName" className="border" type="text" value={lastName} onChange={(e) => setLastName((e.target as HTMLInputElement).value)} />
        <DefaultButton onClick={handleSave}>
          Save
        </DefaultButton>

        <DefaultButton color="error" onClick={handleDelete}>
          Delete
        </DefaultButton>
    </div>
  );
};

export default memo(Users);