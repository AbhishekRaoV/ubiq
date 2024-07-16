provider "aws" {
  region = "us-east-1"
}

resource "aws_instance" "example" {
  ami           = "ami-09e67e426f25ce0d7" // Ubuntu 20.04 LTS 64-bit AMI, you may need to change this for other regions
  instance_type = "t2.medium"
  key_name      = "nextgen-devops-team"
  subnet_id     = "subnet-02cf2e19298b8cdac"
  vpc_security_group_ids = ["sg-0d56e86ef61a7dc01"]

  tags = {
    Name = "auctane-demo"
  }
}

output "instance_ip" {
  value = aws_instance.example.private_ip
}
