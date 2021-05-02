<template>
    <v-icon :color="Color" size="50">{{ Icon }}</v-icon>
</template>

<script lang="ts">
    import { Component, Vue, Prop } from 'vue-property-decorator';

    @Component({
        components: {
        },
    })
    export default class TurnSignal extends Vue {
        @Prop(Number) id = this.Id;
        @Prop(String) icon = this.Icon;

        signals = 0;
        color = "white";
        flashing = false;

        mounted() {
            window.addEventListener("message", (e) => {
                switch (e.data.eventName) {
                    case "TURN_SIGNAL_STATE":
                        this.Signals = e.data._state;
                        break;
                    default:
                        break;
                }
            });
        }

        async State() {
            console.log(this.Signals + ":" + this.Id);
            while (this.Id === this.Signals) {
                this.Color = "green";
                await this.delay(500);
                this.Color = "black";
                await this.delay(500);
            }

            if (this.Color != "white") {
                this.Color = "white"
            }

            this.Flashing = false;
        }

        delay(ms: number) {
            return new Promise(resolve => setTimeout(resolve, ms));
        }

        get Id() {
            return this.id;
        }

        set Id(value: number) {
            this.id = value;
        }

        get Icon() {
            return this.icon;
        }

        set Icon(value: string) {
            this.icon = value;
        }

        get Signals() {
            return this.signals;
        }

        set Signals(value: number) {
            this.signals = value;

            console.log(this.Flashing);
            if (!this.Flashing && this.Id === this.Signals) {
                this.State();
                this.Flashing = true;
            }
        }

        get Color() {
            return this.color;
        }

        set Color(value: string) {
            this.color = value;
        }

        get Flashing() {
            return this.flashing;
        }

        set Flashing(value: boolean) {
            this.flashing = value;
        }
    }
</script>