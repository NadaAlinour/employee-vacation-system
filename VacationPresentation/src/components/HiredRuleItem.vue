<template>
    <div class="hire-rule-container">

      

        <label>Days</label>
        <input type="number" min="1" placeholder="1" />

        <label>From <small>in years</small></label>
        <input type="number" />
        <label>To <small>in years</small></label>
        <input type="number" />


    </div>
</template>

<script>
    export default {

        data() {
            return {
                
            }
        },
    }

</script>

<style>
    .hire-rule-container {
        display: flex;
        flex-direction: row;
        border-bottom: solid;
        margin-bottom: 20px;
        border-color: rgba(133, 135, 145, 0.3);
    }

    .hire-rule-container label {
        margin-right: 10px;
    }

    .hire-rule-container input {
        margin-right: 20px;
    }

</style>