<template>
    <div class="modal-overlay" @click="$emit('close-modal')">
        <div class="modal" @click.stop>
            <h2 class="form-title">Vacation Rules</h2>

            <ul v-if="!flag" class="rule-list">
                <li>Number of times: {{rule.numberOfTimes}}</li>

                <li v-if="rule.duration > 1">Duration: {{rule.duration}} days.</li>
                <li v-else-if="rule.duration == 1">Duration: {{rule.duration}} day.</li>

                <li v-if="rule.unitID">Per: {{rule.unitName}}.</li>

                <li v-if="rule.startDate != null">Start date: {{(rule.startDate).split('T')[0]}}</li>
                <li v-else-if="rule.startDate == null">Start date: none.</li>

                <li v-if="rule.endDate != null">End date: {{(rule.endDate).split('T')[0]}}</li>
                <li v-else-if="rule.endDate == null">End date: none.</li>
            </ul>


            <ul v-else v-for="h in hire" class="rule-list">
                <li>- Number of days: {{h.numberOfDays}}</li>
                <li>Hired between {{h.startYears}} and {{h.endYears}} years.</li>
            </ul>


            <button class="exit-btn" type="button" @click="$emit('close-modal')">Exit</button>
        </div>
    </div>
</template>


<script>
    export default {
   
        props: ['rule', 'hire', 'flag'],

    }
</script>

<style>
    
    .modal {
        text-align: center;
        background-color: white;
        height: 300px;
        width: 500px;
        margin-top: 7%;
        margin-left: 15%;
        padding: 60px 0;
        border-radius: 20px;
    }

    .rule-list li{
        margin-bottom: 10px;
    }

    .exit-btn {
        margin-top: 20px;
    }

</style>