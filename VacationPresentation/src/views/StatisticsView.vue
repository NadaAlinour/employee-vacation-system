<template>
    <div class="statistics-container">
        <div class="stats-container-b">
            <h2 class="form-title">Statistics</h2>


            <div class="parent">
                <label>From</label>
                <input type="date"
                       v-model="fromDate"
                       :class="fromErr ? 'errField' : ''" />
                <label>To</label>
                <input type="date"
                       v-model="toDate"
                       :class="toErr ? 'errField' : ''" />
                <button class="chart-btn" @click="handleGenerate">Generate chart</button>


            </div>
        </div>

        <div class="err-msg-cont">
            <h3>{{this.errMsg}}</h3>
        </div>



        <div class="chart" v-if="vacationNames[0]">
            <BarchartItem :names="vacationNames" :counts="vacationCounts" :key="chartKey"></BarchartItem>
        </div>

    </div>

</template>


<script>

    import BarchartItem from '../components/BarchartItem.vue'
    import { getStatistics, getStatisticsWithRange } from "../utils/http";
    export default {


        components: {
            BarchartItem
        },

        async mounted() {

            const settings = {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${this.$store.state.token}`
                }
            }

            try {
                const data = await getStatistics(this.$store.state.token);
                console.log(data);
                for (var i = 0; i < data.length; i++) {
                    this.vacationNames[i] = data[i].name;
                    this.vacationCounts[i] = data[i].numberOfEmployees;
                }
            } catch (error) {
                console.log(error);
            }

        },

        methods: {

            forceRerender() {
                this.chartKey += 1;
            },


            async handleGenerate() {

                console.log("generate chart was clicked");

                this.errMsg = "";
                this.toErr = false;
                this.fromErr = false;

                console.log(this.fromDate, this.toDate)

                if (this.fromDate == null || this.fromDate == "") {
                    this.fromErr = true;
                }

                if (this.toDate == null || this.toDate == "") {
                    this.toErr = true;
                }

                if (this.fromErr == true || this.toErr == true) {
                    this.errMsg = "Please fill in all the fields.";
                    return;

                } else if (this.fromDate > this.toDate) {
                    this.errMsg = "Start date must be smaller than end date.";
                    this.toErr = true;
                    this.fromErr = true;
                    return;
                }

                // reset array values
                this.vacationCounts = [];
                this.vacationNames = [];

                var fromDate = this.fromDate;
                var toDate = this.toDate;


                const body = {
                    StartDate: fromDate,
                    EndDate: toDate
                }

                try {
                    const response = await getStatisticsWithRange(this.$store.state.token, body);

                    if (response.status === 401) {
                        console.log("action unauthorized, navigate to unauthorized page");
                        this.$router.push('/401');
                        return;
                    }

                    const data = response.data;

                    console.log(data);
                    for (var i = 0; i < data.length; i++) {
                        this.vacationNames[i] = data[i].name;
                        this.vacationCounts[i] = data[i].numberOfEmployees;
                    }
                } catch (error) {
                    console.log(error.message);
                }


                this.forceRerender();

            }
        },


        data() {
            return {
                fromDate: "",
                toDate: "",

                vacationNames: [],
                vacationCounts: [],

                chartKey: 0,

                errMsg: "",
                fromErr: false,
                toErr: false,


            }
        }

    }
</script>

<style>

    .parent {
        display: flex;
        justify-content: center;
    }

    .statistics-container {
        width: 85%;
        height: 100vh;
        position: absolute;
        right: 0;
        background-color: white;
        text-align: center;
    }

        .statistics-container h2 {
            margin: auto;
            margin-top: 20px;
            margin-bottom: 20px;
        }

    .stats-container-b label {
        margin-right: 5px;
        margin-top: 15px;
    }

    .from-input {
        margin-right: 20px;
    }

    .stats-container-b input[type=date] {
        padding: 15px;
        border: 1px solid rgba(0,0,0,.2);
    }

    .chart {
        margin: 50px;
        margin-top: 0px;
    }

    .chart-btn {
        margin-left: 20px;
        padding-top: 10px;
        padding-bottom: 10px;
    }

    .err-msg-cont h3 {
        color: #f54245;
    }

    .statistics-container .err-msg-cont {
        margin-top: 20px;
    }

    .statistics-container input {
        margin-right: 10px;
    }
</style>