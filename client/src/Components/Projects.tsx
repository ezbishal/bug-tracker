import React, { FC, useState, useEffect } from "react";
import axios, { AxiosResponse } from "axios";
import ProjectModel from "../Models/ProjectModel";
import { DefaultButton } from "@fluentui/react";

const Projects: FC = () => {
  const baseUrl: string = "https://localhost:7109/api";
  const [projects, setProjects] = useState<ProjectModel[]>([]);
  const [selectedProject, setSelectedProject] = useState<ProjectModel>();

  useEffect(() => {
    try {
      axios
        .get<ProjectModel[]>(`${baseUrl}/projects`)
        .then((response: AxiosResponse<ProjectModel[]>) => {
          setProjects(response.data);
          setSelectedProject(response.data[0]);
        });
    } catch (error) {
      console.error("Failed to fetch projects:", error);
    }
  }, []);

  function handleSave(event: React.MouseEvent<HTMLButtonElement, MouseEvent>): void
  {
    axios.post(`${baseUrl}/projects`, selectedProject)
      .then((response: AxiosResponse<string>) => {
        alert(response.data);
        window.location.href = "/";
      });
  }

  function handleDelete(event: React.MouseEvent<HTMLButtonElement, MouseEvent>): void
  {
    axios.delete(`${baseUrl}/projects/${selectedProject?.id}`)
      .then((response: AxiosResponse<string>) => {
        alert(response.data);
        window.location.href = "/";
      });
  }

  return (
    <>
      <div className="flex flex-col gap-5 m-5">
        <h3 className="text-[20px]">Projects</h3>
        {projects.map((project: ProjectModel) => (
          <div key={project.id}>
            <a
              href={`/projects/${project.id}`}
              className="block max-w-sm p-6 bg-white border border-gray-200 rounded-lg shadow hover:bg-gray-100 dark:bg-gray-800 dark:border-gray-700 dark:hover:bg-gray-700"
            >
              <h5 className="mb-2 text-2xl font-bold tracking-tight text-gray-900 dark:text-white">
                {project.name}
              </h5>
              <p className="font-normal text-gray-700 dark:text-gray-400">
                {project.description}
              </p>
            </a>
          </div>
        ))}
      </div>
      <DefaultButton onClick={handleSave}>Save</DefaultButton>
      <DefaultButton onClick={handleDelete}>Delete</DefaultButton>
    </>
  );
};

export default Projects;
