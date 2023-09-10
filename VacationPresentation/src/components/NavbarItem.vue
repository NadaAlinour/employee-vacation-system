<template>
    <header>
        <nav class="top-navbar">
            <ul class="top-nav-list">
                <li><h1 class="nav-title">Employee Vacation Management System</h1></li>
                <li v-if="this.$store.state.email"><button @click="logout">Logout</button></li> <!--render conditionally later-->
            </ul>
        </nav>
    </header>

</template>

<script>

    export default {
        methods: {
           
        }
    }
</script>

<style>

    .top-navbar {
        height: 60px;
        background-color: white;
        position: relative;
        border: 1px solid rgba(0,0,0,.2);
        position: sticky;
        top: 0;
        padding: 20px;
    }
    
    .top-nav-list {
        display: flex;
        flex-direction: row;
        /*align-items: center;*/
        justify-content: space-between;
        padding-left: 10px;
        padding-right: 10px;
        margin-top: 10px;
        margin-left: 20px;
        margin-right: 20px;
        margin-bottom: 20px;
        background-color: white;
    }

    
    li {
        list-style: none;
        background-color: white;
    }

    .nav-title {
        background-color: white;
    }

    

    
    


    
</style>