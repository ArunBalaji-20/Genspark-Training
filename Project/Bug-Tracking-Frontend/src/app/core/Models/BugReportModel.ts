

export class BugReportModel {
    constructor(
        public BugName: string = "",
        public Description: string = "",
        public Screenshot: string = "",
        public CvssScore: number = 0,
    ) {}
}

