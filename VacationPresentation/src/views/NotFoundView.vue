<template>
    <div class="notfound-container">
        <div class="image-container">
            <img src="/src/assets/3828537.jpg" />
        </div>
        <p>Page not found :(</p>
        <router-link to="/employees"><button>Go Home</button></router-link>
    </div>
    

</template>

<script>

</script>

<style>

    .notfound-container {
        height: 100vh;
        width: 100%;
        z-index: 1;
        position: relative;
        text-align: center;
        top: 0;
        position: absolute;
    }

    .image-container {
        width: 35%;
        margin: auto;
    }

    .notfound-container img {
        width: 100%;
        margin-top: 20px;
    }

    .notfound-container p {
        color: #1C2E35;
        font-size: 3rem;
        font-weight: bold;
        margin-bottom: 30px;
        font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
    }

    .notfound-container button {
        border-style: solid;
        border-color: #1C2E35;
        color: white;
        font-weight: bold;
        font-size: 1rem;
        width: 150px;
        height: 70px;

    }

   

</style>