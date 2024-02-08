<template>
    <Bar id="my-chart-id"
         :options="chartOptions"
         :data="chartData" />



</template>

<script>
    import { Bar } from 'vue-chartjs'
    import { Chart as ChartJS, Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale } from 'chart.js'

    ChartJS.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale)

    export default {
        name: 'BarChart',
        components: { Bar },

        props: ['names', 'counts'],


        data() {
            return {

              

                chartData: {
                    labels: this.names, // labels will be vacation names prop
                    datasets: [{ data: this.counts }] // data will be number of employees prop
                        
                },
                    

                chartOptions: {
                    responsive: true,
                    scales: {
                        y: {
                            ticks: {
                                beginAtZero: true, // Start at zero
                                stepSize: 1 // Increase by 1
                            }
                        }
                    }
                },

            }
        }
    }
</script>