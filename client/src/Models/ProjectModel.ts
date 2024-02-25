interface ProjectModel {
  id: string;
  name: string;
  authorId: string;
  description: string;
  startDate: Date;
  endDate: Date | null;
  repositoryLink: string;
}

export default ProjectModel;
