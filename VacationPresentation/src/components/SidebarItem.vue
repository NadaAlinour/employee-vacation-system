<template>
    
    <div class="sidebar-container">
        <ul class="side-nav-list">

            <router-link to="/employees"><li>View Employees</li></router-link>
            <router-link to="/addEmployee"><li>Add Employees</li></router-link>


            <router-link to="/vacations"><li>View Vacations</li></router-link>
            <router-link to="/addVacation"><li>Add Vacations</li></router-link>


            <router-link to="/requests"><li>Requests</li></router-link>

            <router-link to="/statistics"><li class="stat-item">Statistics</li></router-link>

            <router-link to="/reports"><li>Reports</li></router-link>



            <li @click="logout">
                Logout
            </li>


        </ul>

    </div>
    

</template>

<script>
    export default {
        name: 'SidebarItem',

        methods: {
            logout() {
                localStorage.removeItem('user')
                localStorage.removeItem('token')
                this.$store.commit('logoutUser')
                console.log(this.$route.name)
                this.$router.push('/')
            }
        }
    }
</script>

<style>


    /*.sidebar-container {
        width: 15%;
    }*/

    .router-link-active li {
        background-color: #349eeb;
        color: white;
    }

    .side-nav-list {
        top: 0;
        background-color: white;
        width: 250px;
        height: 100%;
        position: fixed;
        box-shadow: 0px 0px 10px 1px #bdbcbc;
        text-align: left;
        z-index: 1;
    }

        .side-nav-list li {
            font-size: 1.3rem;
            width: 100%;
            padding: 30px 0;
            /*box-shadow: 0px 0px 10px 1px #bdbcbc;*/
            border-bottom-style: none;
            transition: 0.2s;
            border-color: lightgrey;
            text-indent: 10px;
        }



            .side-nav-list li:nth-child(8) {
                cursor: pointer;
                position: fixed;
                bottom: 0;
                width: 250px;
                text-align: center;
                border-top-style: solid;
                background-color: #909ba3;
                color: white;
                font-weight: 500;
                text-indent: 0;
            }

            .side-nav-list li:nth-child(7):hover {
                background-color: #6c747a;
            }

            .side-nav-list li:hover {
                background-color: #1b5885;
                color: white;
            }




</style>