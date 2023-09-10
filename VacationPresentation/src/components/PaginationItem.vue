<template>
    <div class="pagination-container">
        <a href="#">&laquo;</a>
        <a href="#">1</a>
        <a href="#" class="active">2</a>
        <a href="#">3</a>
        <a href="#">4</a>
        <a href="#">5</a>
        <a href="#">6</a>

        <a href="#">&rsaquo;</a>

    </div>
</template>


<style>

    .pagination-container {
        border-style: none;
        border-color: lightgrey;
        border-radius: 10px;
        padding: 10px;
        width: 50%;
        margin: auto;
        text-align: center;
        background-color: white;
    }

    .pagination-container a {
        font-weight: bold;
        color: black;
        padding: 8px 16px;
        text-decoration: none;
        background-color: white;
    }

    .pagination-container a.active {
        background-color: antiquewhite;
        border-radius: 10px;
        color: white;
    }

    .pagination-container a:hover:not(.active) {
        background-color: #ddd;
    }
    
</style>