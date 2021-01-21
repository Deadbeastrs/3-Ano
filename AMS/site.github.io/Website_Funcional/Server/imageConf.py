from PIL import Image

class _Image():

    def __init__(self,img1):
        self.img = Image.open(img1)
        print(self.img)
    def crop_to_aspect(self, aspect, divisor=1, alignx=0.5, aligny=0.5):
        """Crops an image to a given aspect ratio.
        Args:
            aspect (float): The desired aspect ratio.
            divisor (float): Optional divisor. Allows passing in (w, h) pair as the first two arguments.
            alignx (float): Horizontal crop alignment from 0 (left) to 1 (right)
            aligny (float): Vertical crop alignment from 0 (left) to 1 (right)
        Returns:
            Image: The cropped Image object.
        """
        if self.img.width / self.img.height > aspect / divisor:
            newwidth = int(self.img.height * (aspect / divisor))
            newheight = self.img.height
        else:
            newwidth = self.img.width
            newheight = int(self.img.width / (aspect / divisor))
        self.img = self.img.crop((alignx * (self.img.width - newwidth),
                         aligny * (self.img.height - newheight),
                         alignx * (self.img.width - newwidth) + newwidth,
                         aligny * (self.img.height - newheight) + newheight))
        return self.img
