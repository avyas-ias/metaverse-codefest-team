package com.example.metaversecodefestadserver.controllers.dto.request;

public class AdPublisherContextDTO {

    private String publisherId;

    private String[] activities;
    private String ageRating;

    public AdPublisherContextDTO() {
    }

    public String getPublisherId() {
        return this.publisherId;
    }

    public String[] getActivities() {
        return this.activities;
    }

    public String getAgeRating() {
        return this.ageRating;
    }

    public void setPublisherId(String publisherId) {
        this.publisherId = publisherId;
    }

    public void setActivities(String[] activities) {
        this.activities = activities;
    }

    public void setAgeRating(String ageRating) {
        this.ageRating = ageRating;
    }

    public boolean equals(final Object o) {
        if (o == this) return true;
        if (!(o instanceof AdPublisherContextDTO))
            return false;
        final AdPublisherContextDTO other = (AdPublisherContextDTO) o;
        if (!other.canEqual((Object) this)) return false;
        final Object this$publisherId = this.getPublisherId();
        final Object other$publisherId = other.getPublisherId();
        if (this$publisherId == null ? other$publisherId != null : !this$publisherId.equals(other$publisherId))
            return false;
        if (!java.util.Arrays.deepEquals(this.getActivities(), other.getActivities())) return false;
        final Object this$ageRating = this.getAgeRating();
        final Object other$ageRating = other.getAgeRating();
        if (this$ageRating == null ? other$ageRating != null : !this$ageRating.equals(other$ageRating)) return false;
        return true;
    }

    protected boolean canEqual(final Object other) {
        return other instanceof AdPublisherContextDTO;
    }

    public int hashCode() {
        final int PRIME = 59;
        int result = 1;
        final Object $publisherId = this.getPublisherId();
        result = result * PRIME + ($publisherId == null ? 43 : $publisherId.hashCode());
        result = result * PRIME + java.util.Arrays.deepHashCode(this.getActivities());
        final Object $ageRating = this.getAgeRating();
        result = result * PRIME + ($ageRating == null ? 43 : $ageRating.hashCode());
        return result;
    }

    public String toString() {
        return "AdPublisherContextDTO(publisherId=" + this.getPublisherId() + ", activities=" + java.util.Arrays.deepToString(this.getActivities()) + ", ageRating=" + this.getAgeRating() + ")";
    }
}
