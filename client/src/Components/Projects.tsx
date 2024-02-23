import React, { FC, useState, useEffect } from "react";
import axios, { AxiosResponse } from "axios";
import ProjectModel from "../Models/ProjectModel";

const Projects: FC = () => {
  const baseUrl: string = "https://localhost:7109/api";
  const [projects, setProjects] = useState<ProjectModel[]>([]);

  useEffect(() => {
    try {
      axios
        .get<ProjectModel[]>(`${baseUrl}/projects`)
        .then((response: AxiosResponse<ProjectModel[]>) => {
          setProjects(response.data);
        });
    } catch (error) {
      console.error("Failed to fetch projects:", error);
    }
  }, []);

  const onProjectClick = (e: React.MouseEvent) => {};

  return (
    <div className="App">
      <h1 className="text-green-900">Bug Tracker</h1>

      <div>
        <h2>Projects</h2>
        {projects.map((project: ProjectModel) => (
          <div key={project.id}>
            <label>Project Name:</label>
            <h3 className="text-[100px]" onClick={onProjectClick}>
              {project.name}
            </h3>
            <label htmlFor="description">Description:</label>
            <input type="text" id="description" value={project.description} />
            <label htmlFor="startDate">Project Start Date</label>
            <input
              id="startDate"
              type="date"
              disabled
              value={project.startDate.toString()}
            />
            <label htmlFor="endDate">Project End Date</label>
            <input
              id="endDate"
              type="date"
              disabled
              value={project.endDate?.toString()}
            />
          </div>
        ))}
      </div>
    </div>
  );
};

export default Projects;
