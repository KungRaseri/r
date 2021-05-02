<template>
    <v-progress-circular class="fast-transition" :size="CircleSize()" :width="CircleWidth()" :color="Reverse()" :value="Rpm" rotate="90">
        <v-container>
            <v-row class="guage-label" justify="center" :color="Reverse()">{{ Gear }}</v-row>
        </v-container>
    </v-progress-circular>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';

    @Component({
        components: {
        },
    })
    export default class RpmGuage extends Vue {
        rpm = 0;
        gear = "";

        mounted() {
            window.addEventListener("message", (e) => {
                switch (e.data.eventName) {
                    case "VEHICLE_SPEED_MONITOR":
                        this.DataUpdate(e.data);
                        break;
                    default:
                        break;
                }
            });
        }

        Reverse() {
            if (this.Gear === "R") {
                return "red";
            }

            return "white";
        }

        DataUpdate(value: any) {
            this.Rpm = value.rpm;
            this.Gear = value.gear;
        }

        CircleSize() {
            let size = window.innerWidth + window.innerHeight;
            return size * 0.03;
        }

        CircleWidth() {
            let size = window.innerWidth + window.innerHeight;
            return size / 300;
        }

        get Rpm() {
            return this.rpm;
        }

        set Rpm(value: number) {
            this.rpm = value;
        }

        get Gear() {
            return this.gear;
        }

        set Gear(value: string) {
            this.gear = value;
        }
    }
</script>