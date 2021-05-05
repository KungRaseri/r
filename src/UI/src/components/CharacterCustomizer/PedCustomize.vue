<template>
    <v-card v-show="Show" class="panel" dark>
        <v-container class="fill">
            <v-row dense>
                <v-col>
                    <v-btn color="red" :rounded="true" block>Back</v-btn>
                </v-col>
                <v-col>
                    <v-btn color="green" :rounded="true" block @click="sendped(Ped)">Continue</v-btn>
                </v-col>
            </v-row>
            <v-divider class="my-4" />
            <v-container class="scroll">
                <v-expansion-panels>
                    <StyleComponent v-for="item in Components" :key="item" :name="item" />
                </v-expansion-panels>
            </v-container>
        </v-container>
    </v-card>
</template>

<script lang="ts">
    import { Component, Vue, Prop } from 'vue-property-decorator';
    import StyleComponent from './StyleComponent.vue';

    @Component({
        components: {
            StyleComponent
        },
    })
    export default class PedCustomize extends Vue {
        @Prop(Boolean) show = this.Show;

        components = [""];
        $axios: any;

        mounted() {
            window.addEventListener("message", (e) => {
                switch (e.data.eventName) {
                    case "AGGREGATE_COMPONENTS":
                        this.Components = e.data.comps;
                        break;
                    default:
                        break;
                }
            });
        }

        get Show() {
            return this.show;
        }

        set Show(value: boolean) {
            this.show = value;
        }

        get Components() {
            return this.components;
        }

        set Components(value: string[]) {
            this.components = value;
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
        overflow: scroll;
        height: 87%;
    }
</style>