import React, { FC } from "react";
import Projects from "./Projects";
import Users from "./Users";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUser } from "@fortawesome/free-solid-svg-icons";

const Dashboard: FC = () => {
  function handleIconClick(
    event: React.MouseEvent<SVGSVGElement, MouseEvent>
  ): void {
    sessionStorage.removeItem("token");
    window.location.href = "/login";
  }

  return (
    <>
      <div className="flex w-full justify-between">
        <h1 className="text-[30px] font-bold ml-5">Dashboard</h1>
        <FontAwesomeIcon
          icon={faUser}
          className="m-5"
          onClick={handleIconClick}
        />
      </div>
      <div className="flex w-full">
        <Projects />
        <Users />
      </div>
    </>
  );
};

export default Dashboard;
