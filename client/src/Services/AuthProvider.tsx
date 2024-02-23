import React, { createContext, useContext, useState, ReactNode } from "react";

type User = {
  id: number;
  name: string;
  email: string;
} | null;

type AuthContextType = {
  user: User;
  login: (userDetails: User) => void;
  logout: () => void;
};

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [user, setUser] = useState<User>(
    JSON.parse(localStorage.getItem("user") || "null")
  );

  const login = (userDetails: User) => {
    localStorage.setItem("user", JSON.stringify(userDetails));
    setUser(userDetails);
  };

  const logout = () => {
    localStorage.removeItem("user");
    setUser(null);
  };

  return (
    <AuthContext.Provider value={{ user, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export function useAuth() {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
}
