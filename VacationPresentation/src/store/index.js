import { createStore } from 'vuex'

const store = createStore({
    /*state: {
        user: {
            data: { email: '' },
            //token: null
        }
    }, */

    state: {
       
        email: localStorage.getItem('user'),
        token: localStorage.getItem('token'),
        
        

        updateFlag: false, // i dont remember what this is

        employees: [],


    },

    getters: {
        getUser(state) {
            return state.email;
        },

        getToken(state) {
            return state.token;
        },

        employees(state) {
            return state.employees;
        }
    },

    actions: {
        async fetchAllEmployees(context) {
            return fetch('http://localhost:5242/api/employee')
                .then(res => res.json())
                .then(data => {
                    console.log(data);
                    context.commit("setEmployees", data);

                })
        }
    },

    mutations: {

        updateUser(state, userEmail) {
            state.email = localStorage.getItem('user');
        },

        updateToken(state, userToken) {
            state.token = localStorage.getItem('token');
        },

        logoutUser(state) {
            state.email = localStorage.getItem('user') // should return null
            state.token = localStorage.getItem('token')
        },

        setEmployees(state, employees) {
            this.employees = employees;
        }
    },

    modules: {

    }
})
export default store;