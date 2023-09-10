<template>
    <div class="radio-group-1">
        <fieldset id="group1">
            <div class="radio-one">
                <input type="radio" name="group1" @click="renderGroupOne" value="true" v-model="tempRules.isBasedOnHire"/>
                <label>Based on hire date</label>
            </div>

            <div class="radio-two">

                <input type="radio" name="group1" @click="renderGroupOne" value="false" v-model="tempRules.isBasedOnHire"/>
                <label>Other</label>
            </div>

        </fieldset>
    </div>

    <!------------------------------------------------------------------------------------------------->

    <div class="option-group-1" v-if="toggleOptions1 == true">
        <div>

            <HiredRuleItem v-for="index in count" :key="index"></HiredRuleItem>
            <div class="add-btn-container">
                <button id="add-btn" type="button" @click="addHireRule">+</button>
            </div>
        </div>

    </div>

    <div class="option-group-2" v-if="toggleOptions1 == false">
        <label>Number of times</label>
        <input type="number" min="1" placeholder="1" v-model="tempRules.numberOfTimes"/>

        <!------------------------------------------------------------------------------------------------->


        <fieldset id="group2">

            <div class="radio-one">
                <input type="radio" name="group2" @click="renderGroupTwo" value="within period"/>
                <label>Within period</label>
            </div>

            <div class="radio-two">

                <input type="radio" name="group2" @click="renderGroupTwo" value="other" />
                <label>Other</label>
            </div>

        </fieldset>
    </div>

    <!------------------------------------------------------------------------------------------------->


    <div class="option-group-3" v-if="toggleOptions2 == true">
        <label>From</label>
        <input type="date" v-model="tempRules.startDate"/>

        <label>To</label>
        <input type="date" v-model="tempRules.endDate"/>
    </div>

    <div class="option-group-4" v-if="toggleOptions2 == false">
        <!--<label>Number of times</label>
    <input type="number" min="1" placeholder="1" />-->
        <label>Per</label>
        <select v-model="tempRules.unitID">
            <option value="" disabled>Select unit</option>
            <option v-for="unit in this.units" :key="unit.unitID" :value="unit.unitID">{{unit.unitName}}</option>
            
        </select>
    </div>

   
   
</template >

<script>

    import HiredRuleItem from '../components/HiredRuleItem.vue'

    export default {

        props: ['rule'],

        components: {
            HiredRuleItem
        },

        created() {
            this.fetchUnits();
        },

        data() {
            return {
                toggleOptions1: null,
                toggleOptions2: null,
                count: 1,

                tempRules: {
                    numberOfTimes: "",
                    duration: "",
                    unitID: "",
                    startDate: "",
                    endDate: ""
                },

                units: [],
            }
        },

        methods: {

      

            renderGroupOne(e) {
                // console.log("a radio button was clicked with value:", e.target.value)
                if (e.target.value === "false") this.toggleOptions1 = false; // render second option set
                else this.toggleOptions1 = true; // render first option set
            },

            renderGroupTwo(e) {
                if (e.target.value === "other") this.toggleOptions2 = false; // render second option set
                else this.toggleOptions2 = true; // render first option set
            },

            addHireRule() {
                this.count += 1;
            },

            async fetchUnits() {
                try {
                    const response = await fetch('http://localhost:5242/api/unit');
                    const data = await response.json();

                    this.units = data;
                    console.log(data);
                    console.log(this.units);


                } catch (error) {
                    console.log(error.message);
                }

            }
        }

    }

</script>

<style>

    #group1 {
        border-style: none;
        background-color: white;
        margin-top: 15px;
    }

    .radio-one {
        width: 18%;
    }

    .radio-two {
        width: 18%;
        font-style: italic;
    }

    #group2 {
        border-style: none;
    }

    .option-group-1 {
        display: flex;
        flex-direction: row;
    }


    .option-group-2 {
        display: flex;
        flex-direction: column;
    }

    .option-group-3 {
        display: flex;
        flex-direction: column;
    }

    .option-group-4 {
        background-color: white;
        display: flex;
        flex-direction: column;
    }

    .option-group-4 select {
        width: 28%;
        padding: 15px;
        border-radius: 10px;
        margin-bottom: 50px;
    }

    #add-btn {
        border-radius: 50px;
        font-size: 2rem;
        text-align: center;
    }

 
</style>