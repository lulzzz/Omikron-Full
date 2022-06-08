import moment from 'moment';

export interface DateRangePickerModel {
    startDate: moment.Moment;
    endDate: moment.Moment;
}

export interface TenantSummary {
    id: string;
    identifier: string;
    name: string;
    logo: string;
}