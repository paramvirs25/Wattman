import { ContentBase } from './content.base';

export class ContentModel extends ContentBase {
    isDeleted: boolean;
    createdDate: string;
    createdBy: number;
    modifiedDate: string;
    modifiedBy: number;
}
