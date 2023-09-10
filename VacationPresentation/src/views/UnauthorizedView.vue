<template>
    <div class="unauthorized-container">
        <!--<div class="image-container">
        <img src="/src/assets/3828537.jpg" />
    </div>
       -->
        <div class="unauth-msg-container">
            <p id="err-status">401</p>
            <p>Action Unauthorized</p>

            <div class="unauth-btns">
                <router-link to="/"><button>Login</button></router-link>
                <!--<button @click="handleBack">Go Back</button>-->
            </div>
        </div>

    </div>


</template>

<script>
    import { useRouter } from 'vue-router';

    export default {
        methods: {
            handleBack() {
                this.$router.go(-1);
            }
        }
    }


</script>

<style>

    .unauthorized-container {
        height: 100vh;
        width: 100%;
        z-index: 1;
        position: relative;
        text-align: center;
        top: 0;
        position: absolute;
    }

    #err-status {
        color: #349eeb;
        font-size: 8rem;
        font-weight: bold;
        margin-bottom: 30px;
        font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
    }

    .unauthorized-container p {
        color: #349eeb;
        font-size: 2.5rem;
        font-weight: bold;
        margin-bottom: 30px;
        font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
    }

    .unauth-msg-container {
        display: flex;
        flex-direction: column;
        margin-top: 120px;
    }

    .unauth-btns {
        display: flex;
        gap: 50px;
        margin: auto;
    }


    .unauthorized-container button {
        border-style: solid;
        border-color: #1C2E35;
        color: white;
        font-weight: bold;
        font-size: 1rem;
        width: 150px;
        height: 70px;
    }

   /* .image-container {
        width: 35%;
        margin: auto;
    }

    .unauthorized-container img {
        width: 100%;
        margin-top: 20px;
    }

   

    
       */
</style>