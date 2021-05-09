<template>
    <v-card v-if="Show" class="panel" dark>
        <v-container class="fill">
            <v-row dense>
                <v-col>
                    <v-btn color="red" :rounded="true" block @click="goback()">Back</v-btn>
                </v-col>
                <v-col>
                    <v-btn color="green" :rounded="true" block @click="finish()" :disabled="!Selected">Continue</v-btn>
                </v-col>
            </v-row>
            <v-divider class="my-4" />
            <v-container class="scroll">
                <v-row v-for="(value, index) in Characters" :key="index">
                    <v-btn block @click="SetCharacter(value.Id)">
                        {{ value.First + " " + value.Last }}
                    </v-btn>
                </v-row>
            </v-container>
        </v-container>
    </v-card>
</template>

<script lang="ts">
    import { Component, Vue, Prop, Emit } from 'vue-property-decorator';

    @Component({
        components: {
        },
    })
    export default class ExistingCharacter extends Vue {
        @Prop(Boolean) show = this.Show;

        selected = false;
        characters: any[] = [];
        $axios: any;

        mounted() {
            window.addEventListener("message", (e) => {
                switch (e.data.eventName) {
                    case "AGGREGATE_CHARACTERS":
                        this.Characters = e.data._characters;
                        break;
                    default:
                        break;
                }
            });
        }

        SetCharacter(value: string) {
            this.Selected = true;
            let id = value;
            this.$axios
                .post(
                    "http://framework/SET_CHARACTER",
                    {
                        id
                    }
                )
                .catch((error: any) => {
                    console.log("error", error);
                });
        }

        @Emit()
        finish() {
            let customize = false;
            this.$axios
                .post(
                    "http://framework/SAVE_CHARACTER_CUSTOMIZATION",
                    {
                        customize
                    }
                )
                .catch((error: any) => {
                    console.log("error", error);
                });

            return true;
        }

        @Emit()
        goback() {
            if (this.Selected) {
                this.Selected = false;
                this.$axios
                    .post(
                        "http://framework/CHARACTER_SELECT_SETUP",
                        {
                        }
                    )
                    .catch((error: any) => {
                        console.log("error", error);
                    });

                return false;
            }
            return true;
        }

        get Show() {
            return this.show;
        }

        set Show(value: boolean) {
            this.show = value;
        }

        get Characters() {
            return this.characters;
        }

        set Characters(value: any) {
            value.sort((a: any, b: any) => (a.Last > b.Last) ? 1 : -1);
            this.characters = value;
        }

        get Selected() {
            return this.selected
        }

        set Selected(value: boolean) {
            this.selected = value;
        }
    }
</script>

<style scoped>
    .panel {
        position: fixed;
        top: 0;
        right: 0;
        width: 20%;
        height: 100%;
    }

    .fill {
        height: 100%;
    }

    .scroll {
        overflow-x: hidden;
        overflow-y: auto;
        height: 87%;
    }
</style>