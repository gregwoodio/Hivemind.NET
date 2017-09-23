
export class GangerLevelUpReport {
    public gangerName: string;
    public description: string;
    public newSkillFromCategory: any;

    public constructor(partial: Partial<GangerLevelUpReport>) {
        if (partial.gangerName) {
            this.gangerName = partial.gangerName;
        }
        if (partial.description) {
            this.description = partial.description;
        }
        if (partial.newSkillFromCategory) {
            this.newSkillFromCategory = partial.newSkillFromCategory;
        }
    }
}