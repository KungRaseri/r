<template>
    <v-card
        class="bubble"
        :color="ColorFill"
        elevation="0"
    >
        {{ MessageText }}
    </v-card>
</template>

<script lang="ts">
    import { Component, Vue, Prop } from 'vue-property-decorator';

    @Component({
        components: {
            Bubble
        },
    })

    export default class Bubble extends Vue {
        @Prop(String) private ColorFill: string = this.getColorFill;
        @Prop(String) private MessageText: string = this.getMessageText;

        get getColorFill() {
            return this.ColorFill;
        }

        set getColorFill(value: string) {
            this.ColorFill = value;
        }

        get getMessageText() {
            return this.MessageText;
        }

        set getMessageText(value: string) {
            this.MessageText = value;
        }
    }
</script>

<style scoped>
    .bubble {
        font-size: calc((0.3em + 0.3vw) + (0.3em + 0.3vh));
        color: white !important;
        padding: 5px;
        border-radius: 10px !important;
        margin-bottom: 2%;
    }
</style>