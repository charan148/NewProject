import { FuseNavigation } from '@fuse/types';

export const navigation: FuseNavigation[] = [
    {
        id: 'dashboards',
        title: 'Dashboards',
        translate: 'NAV.DASHBOARDS',
        type: 'collapsable',
        icon: 'dashboard',
        children: [
            {
                id: 'myreport',
                title: 'My Report',
                type: 'item',
                url: '/user/myreport'
            },
            {
                id: 'analytics',
                title: 'Analytics',
                type: 'item',
                url: '/user/analytics'
            }
        ]
    },
    {
        id: 'administration',
        title: 'Administration',
        translate: 'NAV.ECOMMERCE',
        type: 'collapsable',
        icon: 'layers',
        hidden: true,
        children: [
            {
                id: 'users',
                title: 'Users',
                type: 'item',
                url: '/administration/users',
                hidden: true,
            },
            {
                id: 'roles',
                title: 'Roles',
                type: 'item',
                url: '/administration/roles',
                hidden: true
            },
            {
                id: 'requestTemplate',
                title: 'Request Template',
                type: 'item',
                url: '/designer/requesttemplate',
                exactMatch: true
            },
            {
                id: 'lookups',
                title: 'Lookups',
                type: 'item',
                url: '/administration/lookup',
                hidden: true
            },
            //{
            //    id: 'designer',
            //    title: 'Designer',
            //    type: 'item',
            //    url: '/designer/designer',
            //    exactMatch: true
            //},
            {
                id: 'security',
                title: 'Security',
                type: 'item',
                url: '/administration/security',
                hidden: true
            }
        ]
    }
];
