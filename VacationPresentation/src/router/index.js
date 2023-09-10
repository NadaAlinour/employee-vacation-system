import { createRouter, createWebHistory } from 'vue-router'
import LoginView from '../views/LoginView.vue'
import UnauthorizedView from '../views/UnauthorizedView.vue'
import NotFoundView from '../views/NotFoundView.vue'
import store from '../store/index.js'


const router = createRouter({
   
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [

        {
            path: '/',
            name: 'login',
            //meta: { requiresAuth: false },

            component: LoginView
        },

        {
            path: '/employees',
            name: 'employees',
            meta: {requiresAuth: true},
            // route level code-splitting
            // this generates a separate chunk (About.[hash].js) for this route
            // which is lazy-loaded when the route is visited.
            component: () => import('../views/EmployeesView.vue')
        },

        {
            path: '/vacations',
            name: 'vacations',
            meta: { requiresAuth: true },

            component: () => import('../views/VacationsView.vue')
        },

        {
            path: '/requests',
            name: 'requests',
            meta: { requiresAuth: true },

            component: () => import('../views/RequestsView.vue')

        },

        {
            path: '/statistics',
            name: 'statistics',
            meta: { requiresAuth: true },

            component: () => import('../views/StatisticsView.vue')

        },

        {
            path: '/addEmployee',
            name: 'addEmployee',
            meta: { requiresAuth: true },

            component: () => import('../views/AddEmployeeView.vue')

        },

        {
            path: '/addVacation',
            name: 'addVacation',
            meta: { requiresAuth: true },

            component: () => import('../views/AddVacationView.vue')

        },

        {
            path: '/reports',
            name: 'reports',
            meta: { requiresAuth: true },

            component: () => import('../views/ReportsView.vue')

        },

        {
            path: '/401', name: 'UnauthorizedView', component: UnauthorizedView
        },

        {
            path: "/404", name: 'NotFoundView', component: NotFoundView
        },

        {
            path: "/:catchAll(.*)", redirect: '/404'
        }


  ]
})


router.beforeEach((to, from) => {
    if (to.meta.requiresAuth && !store.state.email) {
        return {
            path: '/401',
        }
    }

    else if (store.state.email && to.name === 'login') {
        return {
            path: '/employees'
        }
    }
})

export default router
