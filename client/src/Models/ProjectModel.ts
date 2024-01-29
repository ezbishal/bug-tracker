import ProjectStatusEnum from "../Enums/ProjectStatusEnum";

interface ProjectModel {
  id: string;
  name: string;
  authorId: string;
  description: string;
  startDate: Date;
  endDate: Date | null;
  status: ProjectStatusEnum;
  repositoryLink: string;
}

export default ProjectModel;
