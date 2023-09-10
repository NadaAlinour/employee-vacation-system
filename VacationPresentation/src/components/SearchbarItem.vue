<template>
    <!--<img class="search-icon" src="../assets/search-icon.png"/>-->
    <div class="searchbar">
        <input type="text" class="searchbar-field" placeholder="Search here.." />
        <img class="search-icon" src="/src/assets/search-regular.png"/> 
    </div>
</template>

<script>

</script>

<style>

    .searchbar {
        display: flex;
        align-items: center;
        justify-content: center;
    }

    .searchbar-field {
        border-style: solid;
        border-right-style: none;
        border-color: lightgrey;
        padding: 15px;
        width: 250px;
        background-color: white;
        border-width: 1px;
        border-bottom-left-radius: 10px;
        border-top-left-radius: 10px;
    }

    .searchbar-field:focus {
        outline: none;
    }

    .search-icon {
        width: 30px;
        height: 30px;
        padding: 8.5px;
        border-left-style: solid;
        border-left-color: #909ba3;
        background-color: #909ba3;
        border-top-right-radius: 10px;
        border-bottom-right-radius: 10px;
        cursor: pointer;
        transition: 0.2s;
    }

    .search-icon:hover {
        background-color: #6c747a;
    }

</style>